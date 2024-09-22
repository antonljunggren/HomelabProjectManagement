import { Component, createSignal, onCleanup, onMount, Show } from "solid-js";

type CreateSecretProperties = {
    show:boolean, 
    saveCallback:(secretName:string, secretValue:string) => void,
    cancelCallback:() => void,
}

const CreateSecretPrompt:Component<CreateSecretProperties> = (props) => {

    const [secretName, setSecretName] = createSignal('New Secret');
    const [secretValue, setSecretValue] = createSignal('');

    const resetValues = () => {
        setSecretName('New Secret');
        setSecretValue('');
    };

    const cancel = () => {
        resetValues();
        props.cancelCallback();
    };

    const save = () => {
        props.saveCallback(secretName(), secretValue());
        resetValues();
    };

    return(
        <Show when={props.show} fallback={<></>}>
           <div tabindex='-1' class='overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full h-full bg-gray-600 bg-opacity-70'>
                <div class='relative p-4 w-full max-w-md max-h-full left-1/2 -translate-x-1/2 top-28'>
                    <div class='relative bg-gray-700 rounded-lg shadow'>
                        <button class='absolute top-3 end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center' 
                            onclick={cancel}>
                            <svg class='w-3 h-3' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 14 14'>
                                <path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6'/>
                            </svg>
                        </button>
                        <div class='p-5 text-center'>
                            <h3 class='mb-5 text-xl font-normal text-gray-300'>Create a new server secret</h3>
                            
                            <label class="text-xl mr-4">Secret name:</label>
                            <input class='text-xl rounded-md p-1 mb-2 bg-gray-600'
                                type='text'
                                value={secretName()}
                                onInput={(e) => setSecretName(e.currentTarget.value)}/>

                            <label class="text-xl mr-4">Secret value:</label>
                            <input class='text-xl rounded-md p-1 mb-2 bg-gray-600'
                                type='text'
                                value={secretValue()}
                                onInput={(e) => setSecretValue(e.currentTarget.value)}/>

                            <button class='text-white bg-green-600 hover:bg-green-800 rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center'
                                onclick={save}>
                                Save
                            </button>
                            <button class='py-2.5 px-5 ms-3 text-sm text-gray-200 bg-gray-500 rounded-lg border border-gray-500 hover:bg-gray-300 hover:text-gray-700'
                                onclick={cancel}>Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </Show>
    );
};

export default CreateSecretPrompt;