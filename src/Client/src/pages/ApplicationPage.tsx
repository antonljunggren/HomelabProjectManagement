import { useParams } from "@solidjs/router";
import { Component, createMemo, createSignal, Show } from "solid-js";
import { useAppState } from "../components/AppState";

const ApplicationPage : Component = () => {
    const params = useParams();
    const store = useAppState();

    const isCreatingNew = createMemo(() => params.id === 'create');

    const [isEditing, setIsEditing] = createSignal(isCreatingNew());

    const application = createMemo(() => {
        return store?.state.applications.find(a => a.id === params.id);
    });

    const [tempEditApplication, setTempEditApplication] = createSignal({...application()});
    
    return(
        <Show when={application()} fallback={<div><h1 class='text-2xl'>Server not found!</h1></div>}>
            <div>
                <div>
                     {/* Name */}
                     <div>
                        <label class='text-lg'>Application Name</label>
                        <div>
                            <Show when={!isEditing()} fallback={
                                <input class='text-2xl rounded-md p-1 bg-gray-600'
                                type='text'
                                value={tempEditApplication()?.applicationName}
                                onInput={(e) => {}}/>
                            }>
                                <p class='text-2xl p-1'>{application()?.applicationName}</p>
                            </Show>
                        </div>
                    </div>
                </div>
            </div>
        </Show>
    );
};

export default ApplicationPage;