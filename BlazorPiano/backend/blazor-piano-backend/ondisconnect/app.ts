import { APIGatewayProxyEvent, APIGatewayProxyResult } from 'aws-lambda';
import * as aws from "aws-sdk";

/**
 *
 * Event doc: https://docs.aws.amazon.com/apigateway/latest/developerguide/set-up-lambda-proxy-integrations.html#api-gateway-simple-proxy-for-lambda-input-format
 * @param {Object} event - API Gateway Lambda Proxy Input Format
 *
 * Return doc: https://docs.aws.amazon.com/apigateway/latest/developerguide/set-up-lambda-proxy-integrations.html
 * @returns {Object} object - API Gateway Lambda Proxy Output Format
 *
 */

const docClient = new aws.DynamoDB.DocumentClient();
const ConnectionTableName = process.env.TABLE_NAME!;

export const handler = async (event: APIGatewayProxyEvent): Promise<APIGatewayProxyResult> => {
    let response: APIGatewayProxyResult;
    let connectionData;
    let userId = '';
    let color = '';
    let username = '';
    let users = [];

    const deleteParams = {
        TableName: ConnectionTableName,
        Key: {
            connectionId: event.requestContext.connectionId
        }
    };
    
    try {
        connectionData = await docClient.scan({ TableName: ConnectionTableName, ProjectionExpression: 'connectionId,username,color' }).promise();
    } catch (e: any) {
        return { statusCode: 500, body: e.stack };
    }

    const userLeft = connectionData.Items.find((u: any) => u.connectionId === event.requestContext.connectionId);


    try {
        await docClient.delete(deleteParams).promise();
    } catch (err) {
        return { statusCode: 500, body: 'Failed to disconnect: ' + JSON.stringify(err) };
    }

    const apigwManagementApi = new aws.ApiGatewayManagementApi({
        apiVersion: '2018-11-29',
        endpoint: event.requestContext.domainName + '/' + event.requestContext.stage
    });

    const postData = JSON.stringify({
        action: 'player-disconnected',
        user: { username: userLeft.username, color: userLeft.color }
    });
    console.log(userLeft);

    const postCalls = connectionData.Items.map(async (item: any) => {
        const connectionId = item.connectionId;
        try {
            //console.log(connectionId, users);
            if (connectionId !== event.requestContext.connectionId) {


                await apigwManagementApi.postToConnection({ ConnectionId: connectionId, Data: postData }).promise();
            }
        } catch (e: any) {
            if (e.statusCode === 410) {
                console.log(`Found stale connection, deleting ${connectionId}`);
                await docClient.delete({ TableName: ConnectionTableName, Key: { connectionId } }).promise();
            } else {
                throw e;
            }
        }
    });

    try {
        await Promise.all(postCalls);
    } catch (e: any) {
        console.log(e);
        return { statusCode: 500, body: e.stack };
    }


    return { statusCode: 200, body: 'Disconnected.' };
};
