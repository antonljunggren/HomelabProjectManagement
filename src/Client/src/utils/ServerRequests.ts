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

export type createSeverSecretCmd = {
    serverId: string;
    secretName: string;
    secretValue: string;
}

export type updateSeverSecretCmd = {
    serverId: string;
    secretId: string;
    secretValue: string;
}

export type deleteSeverSecretCmd = {
    serverId: string;
    secretId: string;
}

export const createServer = async (cmd:createServerCmd) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/create", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cmd),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
}

export const updateServer = async (cmd:updateServerCmd) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/update", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cmd),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
};

export const deleteServer = async (serverId:string) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/delete/"+serverId, {
            method: 'POST'
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
}

export const createServerSecret = async (cmd:createSeverSecretCmd) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/secrets/add", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cmd),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
};

export const updateServerSecret = async (cmd:updateSeverSecretCmd) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/secrets/update", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cmd),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
};

export const deleteServerSecret = async (cmd:deleteSeverSecretCmd) => {
    try {
        const response = await fetch("http://localhost:5157/api/server/secrets/delete", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cmd),
        });

        if(!response.ok) {
            throw new Error(response.statusText);
        }

        return await response.json();

    } catch (error) {
        
    }
};