import { Accessor, createSignal, For, Match, Setter, Switch, type Component } from 'solid-js';
import { useAppState } from './AppState';

enum SelectedTab {
    Servers,
    Applications,
    Projects
}

const NavbarTab : Component<{tabType: SelectedTab, selectedTab: Accessor<SelectedTab>, setSelectedTab: Setter<SelectedTab>, tabName: string}> = (props) => {

    const isSelected = () => props.tabType === props.selectedTab();

    return(
        <li class='me-2'>
            <a href='#' class={`p-3 rounded-t-lg hover:bg-gray-700 ${isSelected() && 'bg-gray-700 text-blue-500'}`}
                onclick={() => props.setSelectedTab(props.tabType)}>{props.tabName}</a>
        </li>
    );
}

const Navbar : Component = () => {
    const[selectedTab, setSelectedTab] = createSignal(SelectedTab.Servers);
    const store = useAppState();
    return(
        <div class='pl-2 pt-2 bg-gray-800 max-w-80 text-gray-200'>
            <div class='flex justify-between'>
                <h1 class='text-2xl'>Homelab</h1>
                <p class='text-md p-2 text-blue-400 hover:bg-gray-700 rounded-lg'><a href='/'>Go To Homepage</a></p>
            </div>
            
            <ul class='mx-2 py-3 flex border-b border-gray-200'>
                <NavbarTab tabType={SelectedTab.Servers} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Servers' />
                <NavbarTab tabType={SelectedTab.Applications} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Applications' />
                <NavbarTab tabType={SelectedTab.Projects} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Projects' />
            </ul>
            
            <div class='mx-2'>
                <Switch fallback={<></>}>
                    <Match when={selectedTab() === SelectedTab.Servers}>
                        <div>
                            <button class='my-2 p-1 rounded-md bg-blue-500 fill-white hover:bg-blue-600'>
                                <a href='/server/create' class='flex space-x-2 justify-start items-center text-sm'>
                                    <svg class='w-3 h-3' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M11 11v-11h1v11h11v1h-11v11h-1v-11h-11v-1h11z"/></svg>
                                    <p>Add Server</p>
                                </a>
                            </button>
                        </div>
                        
                        <ul>
                            <For each={store?.state.servers} fallback={<></>}>
                                {(server, index) => (
                                    <li class='hover:underline pl-1'><a href={`/server/${server.id}`}>{server.name} - {server.ipAddress}</a></li>
                                )}
                            </For>
                        </ul>
                    </Match>
                    <Match when={selectedTab() === SelectedTab.Applications}>
                        <p>Applications geegaaafwf fefewfef ejkfbswhjfbjfbefbf</p>
                    </Match>
                    <Match when={selectedTab() === SelectedTab.Projects}>
                        <p>Projects gehrthaw fefsesrgg tyhhr fefewfef ejkfbswhjfbjfbefbf</p>
                    </Match>
                </Switch>
            </div>
        </div>
    );
};

export default Navbar;