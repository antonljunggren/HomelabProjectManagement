import { useNavigate, useParams } from '@solidjs/router';
import { createEffect, createMemo, createSignal, For, Show, type Component } from 'solid-js';
import { useAppState } from '../components/AppState';
import { Server, ServerSecret, ServerSpecification } from '../models/Server';
import { createServerCmd, createServerSecretCmd, deleteServerSecretCmd, updateServerCmd, updateServerSecretCmd } from '../utils/Requests';
import DeletePrompt from '../components/DeletePrompt';
import CreateSecretPrompt from '../components/CreateSecretPrompt';

const ServerPage : Component = () => {
    const params = useParams();
    const store = useAppState();
    const navigate = useNavigate();

    const isCreatingNew = createMemo(() => params.id === 'create');

    const [isEditing, setIsEditing] = createSignal(isCreatingNew());

    createEffect(() => setIsEditing(isCreatingNew()), isCreatingNew());

    const server = createMemo(() => {
        if(isCreatingNew()) {
            const newServerTemp:Server = {
                id: '0',
                name: 'new server',
                ipAddress: '',
                serverSpecifications: {
                    cpu: '',
                    ramGigagabytes: 0,
                    diskSizeGigabytes: 0
                },
                secrets: []
            }

            return newServerTemp;
        } else {
            return store?.state.servers.find(s => s.id === params.id);
        }
    });

    createEffect(() => {
        setTempEditServer({...server()});
    }, server);

    const [tempEditServer, setTempEditServer] = createSignal({...server()});
    const [tempSecretToEdit, setSecretToEdit] = createSignal<ServerSecret>();

    const [showDeleteSecretModal, setShowDeleteSecretModal] = createSignal(false);
    const [showCreateSecretModal, setShowCreateSecretModal] = createSignal(false);
    const [showDeleteServerModal, setShowDeleteServerModal] = createSignal(false);
    const [secretToDelete, setSecretToDelete] = createSignal('');

    const handleServerSave = () => {
        if(isCreatingNew() && tempEditServer()) {
            const cmd: createServerCmd = {
                serverName: tempEditServer()?.name ?? '',
                ipAddress: tempEditServer()?.ipAddress ?? '',
                cpu: tempEditServer().serverSpecifications?.cpu ?? '',
                ramGigagabytes: tempEditServer().serverSpecifications?.ramGigagabytes ?? 0,
                diskSizeGigabytes: tempEditServer().serverSpecifications?.diskSizeGigabytes ?? 0
            };

            const createdServer = store?.serverActions.createServer(cmd);
            createdServer?.then((s) => {
                navigate('/server/'+s.id, {replace: true});
            });

        } else if(tempEditServer()) {

            const cmd: updateServerCmd = {
                serverId: tempEditServer().id ?? '',
                serverName: tempEditServer().name ?? '',
                ipAddress: tempEditServer().ipAddress ?? '',
                cpu: tempEditServer().serverSpecifications?.cpu ?? '',
                ramGigagabytes: tempEditServer().serverSpecifications?.ramGigagabytes ?? 0,
                diskSizeGigabytes: tempEditServer().serverSpecifications?.diskSizeGigabytes ?? 0,
            };

            store?.serverActions.updateServer(cmd);
            setIsEditing(false);
        }
    };

    const handleDeleteServer = () => {
        if(server()) {
            store?.serverActions.deleteServer(server()?.id ?? '');
        }
    }

    const handleSecretSave = () => {
        if(tempSecretToEdit()) {
            const cmd : updateServerSecretCmd = {
                serverId: server()?.id ?? '',
                secretId: tempSecretToEdit()?.id ?? '',
                secretValue: tempSecretToEdit()?.secretValue ?? ''
            }

            store?.serverActions.updateServerSecret(cmd);
            setSecretToEdit(undefined);
        }
    };

    const deleteServerModalCallback = (toDelete:boolean) => {
        if(toDelete) {
            handleDeleteServer();
            navigate('/', {replace: true});
        }

        setShowDeleteServerModal(false);
    };

    const createSecretModalCallback = (secretName:string, secretValue:string) => {
        const cmd: createServerSecretCmd = {
            serverId: server()?.id ?? '',
            secretName: secretName,
            secretValue: secretValue
        };

        store?.serverActions.createServerSecret(cmd);
        setShowCreateSecretModal(false);
    };

    const deleteSecretModalCallback = (toDelete:boolean) => {
        if(toDelete) {
            handleDeleteServerSecret(secretToDelete());
        }

        setShowDeleteSecretModal(false);
    };

    const handleDeleteServerSecret = (secretId: string) => {
        if(server()) {

            const cmd : deleteServerSecretCmd = {
                serverId: server()?.id ?? '',
                secretId: secretId,
            }

            store?.serverActions.deleteServerSecret(cmd);
        }
    };

    const handleChange = (key: keyof Server | keyof ServerSpecification | keyof ServerSecret, value: string | number) => {
        setTempEditServer((prev) => {
            
            if(!prev) {
                return prev;
            }

            const specs : ServerSpecification = prev.serverSpecifications ?? {
                cpu: '',
                ramGigagabytes: 0,
                diskSizeGigabytes: 0
            }

            if(key in specs) {
                return {
                    ...prev,
                    serverSpecifications: {
                        ...specs,
                        [key]: value
                    }
                }
            }

            return {...prev, [key]: value}
        });
    };

    const handleServerSecretChange = (key: keyof ServerSecret, value: string) => {
        setSecretToEdit((prev) => {
            if(!prev) {
                return prev;
            }

            return {...prev, [key]: value}
        });
    };

    return(
        <Show when={server()} fallback={<div><h1 class='text-2xl'>Server not found!</h1></div>}>
            <div class='flex justify-between'>
                <div>
                    {/* Name */}
                    <div>
                        <label class='text-lg'>Name</label>
                        <div>
                            <Show when={!isEditing()} fallback={
                                <input class='text-2xl rounded-md p-1 bg-gray-600'
                                type='text'
                                value={tempEditServer()?.name}
                                onInput={(e) => handleChange('name', e.currentTarget.value)}/>
                            }>
                                <p class='text-2xl p-1'>{server()?.name}</p>
                            </Show>
                        </div>
                    </div>
                     {/* IP Address */}
                     <div>
                        <label class='text-lg'>IP Address</label>
                        <div>
                            <Show when={!isEditing()} fallback={
                                <input class='text-2xl rounded-md p-1 bg-gray-600'
                                type='text'
                                value={tempEditServer()?.ipAddress}
                                onInput={(e) => handleChange('ipAddress', e.currentTarget.value)}/>
                            }>
                                <p class='text-2xl p-1'>{server()?.ipAddress}</p>
                            </Show>
                        </div>
                    </div>
                     {/* Specifications */}
                     <div class='mt-4'>
                        <label class='text-lg'>Specifications</label>
                        <div class='p-1 ml-1'>
                            <Show when={!isEditing()} fallback={
                                <div>
                                    <div class='flex items-center space-x-4'>
                                        <p class='text-sm w-16'>CPU</p>
                                        <input class='text-2xl rounded-md p-1 bg-gray-600'
                                        type='text'
                                        value={tempEditServer()?.serverSpecifications?.cpu}
                                        onInput={(e) => handleChange('cpu', e.currentTarget.value)}/>
                                    </div>

                                    <div class='flex items-center space-x-4 mt-1'>
                                        <p class='text-sm w-16'>RAM Size</p>
                                        <input class='text-2xl rounded-md p-1 bg-gray-600'
                                        type='number'
                                        value={tempEditServer()?.serverSpecifications?.ramGigagabytes}
                                        onInput={(e) => handleChange('ramGigagabytes', e.currentTarget.value)}/>
                                        <p class='text-2xl'>GB</p>
                                    </div>

                                    <div class='flex items-center space-x-4 mt-1'>
                                        <p class='text-sm w-16'>Disk Size</p>
                                        <input class='text-2xl rounded-md p-1 bg-gray-600'
                                        type='number'
                                        value={tempEditServer()?.serverSpecifications?.diskSizeGigabytes}
                                        onInput={(e) => handleChange('diskSizeGigabytes', e.currentTarget.value)}/>
                                        <p class='text-2xl'>GB</p>
                                    </div>
                                </div>
                            }>
                                <div class='flex items-center space-x-4'>
                                    <p class='text-sm w-16'>CPU</p>
                                    <p class='text-2xl p-1 ml-4'>{server()?.serverSpecifications.cpu}</p>
                                </div>

                                <div class='flex items-center space-x-4 mt-1'>
                                    <label class='text-sm w-16'>RAM Size</label>
                                    <p class='text-2xl p-1 ml-4'>{server()?.serverSpecifications.ramGigagabytes} GB</p>
                                </div>

                                <div class='flex items-center space-x-4 mt-1'>
                                    <label class='text-sm w-16'>Disk Size</label>
                                    <p class='text-2xl p-1 ml-4'>{server()?.serverSpecifications.diskSizeGigabytes} GB</p>
                                </div>
                            </Show>
                        </div>
                    </div>
                </div>

                {/* Buttons */}
                <div>
                    <div class='flex space-x-4'>
                        <Show when={isEditing()} fallback={
                            <>
                                <button class='bg-blue-500 hover:bg-blue-600  text-white px-4 py-2 rounded-md' onClick={() => {
                                    setTempEditServer({ ...server() });
                                    setIsEditing(true);
                                    }}>Edit</button>
                                <button class='bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded-md'
                                    onclick={() => setShowDeleteServerModal(true)}>Delete</button>
                            </>
                        }>
                            <button class='bg-green-500 hover:bg-green-600  text-white px-4 py-2 rounded-md' onClick={handleServerSave}>
                                Save
                            </button>
                            <button class='bg-gray-500 hover:bg-gray-600  text-white px-4 py-2 rounded-md' onClick={() => {
                                if(isCreatingNew()) {
                                    navigate('/', {replace: true});
                                } else {
                                    setIsEditing(false);
                                }
                            }}>Cancel</button>
                        </Show>
                    </div>
                </div>
            </div>

            <hr class='my-4'/>
            
            {/* Secrets */}
            <Show when={!isCreatingNew()} fallback={<></>}>
                <div>
                    <label class='text-xl'>Secrets</label>
                    <button class='my-2 ml-2 p-1 rounded-md bg-blue-500 fill-white hover:bg-blue-600'
                    onclick={() => setShowCreateSecretModal(true)}>
                        <div class='flex space-x-2 justify-start items-center text-sm'>
                            <svg class='w-3 h-3' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M11 11v-11h1v11h11v1h-11v11h-1v-11h-11v-1h11z"/></svg>
                            <p>Add Secret</p>
                        </div>
                    </button>
                    <div class='mt-2 ml-2'>
                        <For each={server()?.secrets} fallback={<></>}>
                            {(secret, index) => (
                                <div class='flex space-x-2'>
                                    <p class='text-xl my-auto'>{secret.secretName} :</p>
                                    <Show when={tempSecretToEdit() && tempSecretToEdit()?.id == secret.id} fallback={
                                        <div class='flex space-x-4'>
                                            <p class='text-xl my-auto p-1'>{secret.secretValue}</p>
                                            <button class='px-4 py-2 text-white bg-blue-500 hover:bg-blue-600 rounded-md'
                                                onclick={() => {
                                                    setSecretToEdit({...secret});
                                                }}>Edit</button>
                                            <button class='px-4 py-2 text-white bg-red-500 hover:bg-red-600 rounded-md'
                                                onclick={() => {
                                                    setShowDeleteSecretModal(true);
                                                    setSecretToDelete(secret.id);
                                                }}>Delete</button>
                                        </div>
                                    }>
                                        <div class='flex space-x-4'>
                                            <input class='text-xl rounded-md p-1 bg-gray-600'
                                                type='text'
                                                value={tempSecretToEdit()?.secretValue}
                                                onInput={(e) => handleServerSecretChange('secretValue', e.currentTarget.value)}/>
                                            <button class='px-4 py-2 text-white bg-green-500 hover:bg-green-600 rounded-md'
                                                onclick={handleSecretSave}>Save</button>
                                            <button class='px-4 py-2 text-white bg-gray-500 hover:bg-gray-600 rounded-md'
                                                onclick={() => {
                                                    setSecretToEdit(undefined);
                                                }}>Cancel</button>
                                        </div>
                                    </Show>
                                </div>
                            )}
                        </For>
                    </div>
                </div>
            </Show>

            <CreateSecretPrompt show={showCreateSecretModal()} saveCallback={createSecretModalCallback} cancelCallback={() => setShowCreateSecretModal(false)}/>
            <DeletePrompt show={showDeleteServerModal()} infoText='Are you sure you want to delete this server?' callback={deleteServerModalCallback} />
            <DeletePrompt show={showDeleteSecretModal()} infoText='Are you sure you want to delete this secret?' callback={deleteSecretModalCallback} />
        </Show>
    );
};

export default ServerPage;