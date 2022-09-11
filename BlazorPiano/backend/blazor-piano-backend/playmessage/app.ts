import { APIGatewayProxyEvent, APIGatewayProxyResult } from 'aws-lambda';
import * as aws from "aws-sdk";


const docClient = new aws.DynamoDB.DocumentClient();
const ConnectionTableName = process.env.TABLE_NAME!;


export const handler = async (event: APIGatewayProxyEvent): Promise<APIGatewayProxyResult> => {
    let response: APIGatewayProxyResult;
    let connectionData;
    let userId = '';
    let color = '';
    let username = '';    

    try {
        var params = {
            TableName: ConnectionTableName,
            Key: {
                'connectionId': event.requestContext.connectionId
            }
        };

        console.log(params);
        const data = await docClient.get(params).promise();

        console.log(data);
        userId = data.Item.userId;
        color = data.Item.color;
        username = data.Item.username;                

    } catch (e) {
        console.log(e);
        return { statusCode: 500, body: 'FAILED!' };
    }    

    try {
        connectionData = await docClient.scan({ TableName: ConnectionTableName, ProjectionExpression: 'connectionId' }).promise();
    } catch (e:any) {
        return { statusCode: 500, body: e.stack };
    }


    const apigwManagementApi = new aws.ApiGatewayManagementApi({
        apiVersion: '2018-11-29',
        endpoint: event.requestContext.domainName + '/' + event.requestContext.stage
    });

    //const postData = `${userId} said: ${JSON.parse(event.body).data}`;
    const postData = JSON.stringify({
        color: color,
        username: username,
        message: JSON.parse(event.body).data
    });

    const postCalls = connectionData.Items.map(async (item: any) => {
        const connectionId = item.connectionId;

        try {
            console.log(connectionId);
            if (connectionId !== event.requestContext.connectionId) {
                await apigwManagementApi.postToConnection({ ConnectionId: connectionId, Data: postData }).promise();
            }
        } catch (e:any) {
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
    } catch (e:any) {
        console.log(e);
        return { statusCode: 500, body: e.stack };
    }


    return { statusCode: 200, body: 'Data sent.' };
};
