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

    if (event.queryStringParameters) {
        userId = event.queryStringParameters.userId;
        color = event.queryStringParameters.color;
        username = event.queryStringParameters.username;
    }
    const putParams = {
        TableName: ConnectionTableName,
        Item: {
            connectionId: event.requestContext.connectionId,
            userId: userId,
            color: color,
            username: username,
        }
    };

    try {
        await docClient
            .put(putParams)
            .promise();
        //return { statusCode: 200, body: "Connected." };
    } catch (err) {
        console.error(err);
        return { statusCode: 500, body: "Connection failed." };
    }

    const apigwManagementApi = new aws.ApiGatewayManagementApi({
        apiVersion: '2018-11-29',
        endpoint: event.requestContext.domainName + '/' + event.requestContext.stage
    });


    try {
        connectionData = await docClient.scan({ TableName: ConnectionTableName, ProjectionExpression: 'connectionId,username,color' }).promise();
    } catch (e: any) {
        return { statusCode: 500, body: e.stack };
    }

    users = connectionData.Items.map((item: any) => ({ color: item.color, username: item.username, connectionId: item.connectionId }));

    const postCalls = connectionData.Items.map(async (item: any) => {
        const connectionId = item.connectionId
        try {
            //console.log(connectionId, users);
            if (connectionId !== event.requestContext.connectionId) {
                const postData = JSON.stringify({
                    action: 'player-connected',
                    user: { username: username, color: color }
                });

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


    return {
        statusCode: 200,
        body: 'Connected'
    };

};
