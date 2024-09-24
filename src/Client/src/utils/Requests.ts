import { Application } from "../models/Application";
import { Server } from "../models/Server";

export type updateServerCmd = {
    serverId: string;
    serverName: string;
    ipAddress: string;
    cpu: string;
    ramGigagabytes: number;
    diskSizeGigabytes: number;
}

export type createServerCmd = {
    serverName: string;
    ipAddress: string;
    cpu: string;
    ramGigagabytes: number;
    diskSizeGigabytes: number;
}

export type createServerSecretCmd = {
    serverId: string;
    secretName: string;
    secretValue: string;
}

export type updateServerSecretCmd = {
    serverId: string;
    secretId: string;
    secretValue: string;
}

export type deleteServerSecretCmd = {
    serverId: string;
    secretId: string;
}

const baseApiUrl = 'http://localhost:5157/api/';

const apiRequest = async (url:string, method: string = 'GET', body?: any) => {
    const fullUrl = `${baseApiUrl}${url}`;
    try {
        const response = await fetch(fullUrl, {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        if(response.status === 204) {
            return Promise.resolve({});
        }

        return await response.json();

    } catch (error) {
        console.error('API request failed: ', error);
        throw error;
    }
}

export const fetchServersRequest = async () : Promise<Server[]> => 
    apiRequest("server", 'GET');

export const createServerRequest = (cmd: createServerCmd) : Promise<Server> =>
    apiRequest("server/create", 'POST', cmd);

export const updateServerRequest = (cmd: updateServerCmd) =>
    apiRequest("server/update", 'POST', cmd);

export const deleteServerRequest = (serverId: string) =>
    apiRequest(`server/delete/${serverId}`, 'POST');

export const createServerSecretRequest = (cmd: createServerSecretCmd) =>
    apiRequest("server/secrets/add", 'POST', cmd);

export const updateServerSecretRequest = (cmd: updateServerSecretCmd) =>
    apiRequest("server/secrets/update", 'POST', cmd);

export const deleteServerSecretRequest = (cmd: deleteServerSecretCmd) =>
    apiRequest("server/secrets/delete", 'POST', cmd);

export const fetchApplicationsRequest = async () : Promise<Application[]> => 
    apiRequest("application", 'GET');