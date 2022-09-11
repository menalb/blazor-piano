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
        await docClient
            .put(deleteParams)
            .promise();
        return { statusCode: 200, body: "Disconnected." };
    } catch (err) {
        console.error(err);
        return { statusCode: 500, body: "Disconnection failed." };
    }
};
