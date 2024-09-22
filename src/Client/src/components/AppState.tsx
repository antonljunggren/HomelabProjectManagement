import { createStore } from "solid-js/store";
import { Server } from "../models/Server";
import { createContext, JSX, useContext } from "solid-js";
import { createServer, createServerCmd, createServerSecret, createSeverSecretCmd, deleteServer, deleteServerSecret, deleteSeverSecretCmd, updateServer, updateServerCmd, updateServerSecret, updateSeverSecretCmd } from "../utils/ServerRequests";

interface AppState {
    servers: Server[]
}

const [state, setState] = createStore<AppState>({
    servers: []
});

export const AppStateContext = createContext<{
    state: AppState
    fetchData: () => Promise<void>;
    createServer: (cmd: createServerCmd) => Promise<Server>;
    updateServer: (cmd: updateServerCmd) => Promise<void>;
    deleteServer: (serverId: string) => Promise<void>;
    createServerSecret: (cmd: createSeverSecretCmd) => Promise<void>;
    updateServerSecret: (cmd: updateSeverSecretCmd) => Promise<void>;
    deleteServerSecret: (cmd: deleteSeverSecretCmd) => Promise<void>;
}>();

export const AppStateProvider = (props: {children: JSX.Element}) => {
    const fetchData = async () => {
        const response = await fetch("http://localhost:5157/api/server/");
        const data = await response.json();
        
        setState({servers:data});
    };

    const handleCreateServer = async (cmd: createServerCmd) => {
        const data = await createServer(cmd);
        setState(prev => ({
            ...prev,
            servers: [...prev.servers, data]
        }));
        return data;
    };

    const handleUpdateServer = async (cmd: updateServerCmd) => {
        const data = await updateServer(cmd);
        setState(prev => ({
            servers: prev.servers.map(s => 
                s.id === cmd.serverId ? data : s
            )
        }));
    };

    const handleDeleteServer = async (serverId: string) => {
        const data = await deleteServer(serverId);
        setState(prev => ({
            servers: prev.servers.filter(s => s.id !== serverId)
        }));
    };

    const handleCreateServerSecret = async (cmd: createSeverSecretCmd) => {
        const data = await createServerSecret(cmd);
        setState(prev => ({
            servers: prev.servers.map(server => {
                if(server.id === cmd.serverId) {
                    return {
                        ...server,
                        secrets: [...server.secrets, data]
                    };
                }
                return server;
            })
        }));
    };

    const handleUpdateServerSecret = async (cmd: updateSeverSecretCmd) => {
        const data = await updateServerSecret(cmd);
        setState(prev => ({
            servers: prev.servers.map(server => {
                if(server.id === cmd.serverId) {
                    return {...server,
                        secrets: server.secrets.map(secret => {
                            if(secret.id === data.id) {
                                return {...secret, secretValue: data.secretValue};
                            }
                            return secret;
                        })
                    };
                }
                return server;
            })
        }));
    };

    const handleDeleteServerSecret = async (cmd: deleteSeverSecretCmd) => {
        const data = await deleteServerSecret(cmd);
        setState(prev => ({
            servers: prev.servers.map(server => {
                if(server.id === cmd.serverId) {
                    return {
                        ...server,
                        secrets: server.secrets.filter(secret => secret.id !== cmd.secretId)
                    };
                }
                return server;
            })
        }));
    };

    return (
        <AppStateContext.Provider value={{ state, fetchData, 
            createServer:handleCreateServer, updateServer:handleUpdateServer, deleteServer:handleDeleteServer, 
            createServerSecret:handleCreateServerSecret, updateServerSecret:handleUpdateServerSecret, deleteServerSecret:handleDeleteServerSecret }}>
          {props.children}
        </AppStateContext.Provider>
      );
};

export const useAppState = () => {
    const context = useContext(AppStateContext);

    if(!context?.state.servers.length) {
        context?.fetchData();
    }

    return context;
};