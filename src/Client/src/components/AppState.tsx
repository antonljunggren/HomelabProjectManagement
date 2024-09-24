import { createStore } from "solid-js/store";
import { Server } from "../models/Server";
import { createContext, JSX, onMount, useContext } from "solid-js";
import { createServerCmd, updateServerCmd, createServerSecretCmd, updateServerSecretCmd, deleteServerSecretCmd, createServerRequest, updateServerRequest, deleteServerRequest, createServerSecretRequest, updateServerSecretRequest, deleteServerSecretRequest, fetchServersRequest, fetchApplicationsRequest } from "../utils/Requests";
import { Application } from "../models/Application";

interface AppState {
    servers: Server[];
    applications: Application[];
}

interface ServerActions {
    fetchServers: () => Promise<void>;
    createServer: (cmd: createServerCmd) => Promise<Server>;
    updateServer: (cmd: updateServerCmd) => Promise<void>;
    deleteServer: (serverId: string) => Promise<void>;
    createServerSecret: (cmd: createServerSecretCmd) => Promise<void>;
    updateServerSecret: (cmd: updateServerSecretCmd) => Promise<void>;
    deleteServerSecret: (cmd: deleteServerSecretCmd) => Promise<void>;
}

interface ApplicationActions {
    fetchApplications: () => Promise<void>;
}

const [state, setState] = createStore<AppState>({
    servers: [],
    applications: []
});

export const AppStateContext = createContext<{
    state: AppState
    serverActions: ServerActions;
}>();

export const AppStateProvider = (props: {children: JSX.Element}) => {
    
    const fetchServers = async() => {
        try {
            const data = await fetchServersRequest();
            setState({ servers: data });
        } catch (error) {
            console.error("Failed to fetch servers:", error);
        }
    };

    const fetchApplications = async() => {
        try {
            const data = await fetchApplicationsRequest();
            setState({ applications: data });
        } catch (error) {
            console.error("Failed to fetch servers:", error);
        }
    };

    onMount(() => {
        fetchServers();
        fetchApplications();
    });

    //#region Server

    const createServer = async (cmd: createServerCmd) => {
        try {
            const data = await createServerRequest(cmd);
            setState("servers", servers => [... servers, data]);
            return data;
        } catch (error) {
            console.error("Failed to create server:", error);
            throw error;
        }
    };

    const updateServer = async (cmd: updateServerCmd) => {
        try {
            const data = await updateServerRequest(cmd);
            setState("servers", server => server.id === cmd.serverId, data);
            return data;
        } catch (error) {
            console.error("Failed to update server:", error);
        }
    };

    const deleteServer = async (serverId: string) => {
        try {
            await deleteServerRequest(serverId);
            setState("servers", servers => servers.filter(s => s.id !== serverId));
        } catch (error) {
            console.error("Failed to delete server:", error);
        }
    };

    const createServerSecret = async (cmd: createServerSecretCmd) => {
        try {
            const data = await createServerSecretRequest(cmd);
            setState("servers", server => server.id === cmd.serverId, "secrets", secrets => [...secrets, data]);
        } catch (error) {
            console.error("Failed to create server secret:", error);
        }
    };

    const updateServerSecret = async (cmd: updateServerSecretCmd) => {
        try {
            const data = await updateServerSecretRequest(cmd);
            setState("servers", server => server.id === cmd.serverId, "secrets", secret => secret.id === data.id, "secretValue", data.secretValue);
        } catch (error) {
            console.error("Failed to update server secret:", error);
        }
    };
    
    const deleteServerSecret = async (cmd: deleteServerSecretCmd) => {
        try {
            await deleteServerSecretRequest(cmd);
            setState("servers", server => server.id === cmd.serverId, "secrets", secrets => secrets.filter(s => s.id !== cmd.secretId));
        } catch (error) {
            console.error("Failed to delete server secret:", error);
        }
    };

    //#endregion

    //#region Application

    //#endregion

    return (
        <AppStateContext.Provider value={{ state, 
            serverActions: {fetchServers, createServer, updateServer, deleteServer, createServerSecret, updateServerSecret, deleteServerSecret}
            }}>
          {props.children}
        </AppStateContext.Provider>
      );
};

export const useAppState = () => {
    const context = useContext(AppStateContext);

    return context;
};