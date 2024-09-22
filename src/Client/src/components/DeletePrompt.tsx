import { Component, Show } from "solid-js";

type DeletePromptProperties = {
    show:boolean, 
    infoText:string, 
    callback:(confirmation:boolean) => void,
}

const DeletePrompt : Component<DeletePromptProperties> = (props) => {

    return(
        <Show when={props.show} fallback={<></>}>
           <div tabindex='-1' class='overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 justify-center items-center w-full h-full bg-gray-600 bg-opacity-70'>
                <div class='relative p-4 w-full max-w-md max-h-full left-1/2 -translate-x-1/2 top-28'>
                    <div class='relative bg-gray-700 rounded-lg shadow'>
                        <button class='absolute top-3 end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center' 
                            onclick={() => props.callback(false)}>
                            <svg class='w-3 h-3' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 14 14'>
                                <path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6'/>
                            </svg>
                        </button>
                        <div class='p-5 text-center'>
                            <svg class='mx-auto mb-4 text-gray-300 w-12 h-12' aria-hidden='true' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'>
                                <path stroke='currentColor' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M10 11V6m0 8h.01M19 10a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z'/>
                            </svg>
                            <h3 class='mb-5 text-lg font-normal text-gray-300'>{props.infoText}</h3>
                            <button class='text-white bg-red-600 hover:bg-red-800 rounded-lg text-sm inline-flex items-center px-5 py-2.5 text-center'
                                onclick={() => props.callback(true)}>
                                Yes, I'm sure
                            </button>
                            <button class='py-2.5 px-5 ms-3 text-sm text-gray-200 bg-gray-500 rounded-lg border border-gray-500 hover:bg-gray-300 hover:text-gray-700'
                                onclick={() => props.callback(false)}>No, cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </Show>
    );
}

export default DeletePrompt;