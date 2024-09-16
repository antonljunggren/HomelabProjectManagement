import { Accessor, createSignal, Match, Setter, Switch, type Component } from 'solid-js';

enum SelectedTab {
    Servers,
    Applications,
    Projects
}

const NavbarTab : Component<{tabType: SelectedTab, selectedTab: Accessor<SelectedTab>, setSelectedTab: Setter<SelectedTab>, tabName: string}> = (props) => {

    const isSelected = () => props.tabType === props.selectedTab();

    return(
        <li class='me-2'>
            <a href="#" class={`p-3 rounded-t-lg hover:bg-gray-700 ${isSelected() && 'bg-gray-700 text-blue-500'}`}
                onclick={() => props.setSelectedTab(props.tabType)}>{props.tabName}</a>
        </li>
    );
}

const Navbar : Component = () => {
    const[selectedTab, setSelectedTab] = createSignal(SelectedTab.Servers);
    return(
        <div class='pl-2 bg-gray-800 max-w-80 text-gray-200'>
            <h1 class='text-2xl'>Homelab</h1>
            <ul class='mx-2 py-3 flex border-b border-gray-200'>
                <NavbarTab tabType={SelectedTab.Servers} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Servers' />
                <NavbarTab tabType={SelectedTab.Applications} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Applications' />
                <NavbarTab tabType={SelectedTab.Projects} selectedTab={selectedTab} setSelectedTab={setSelectedTab} tabName='Projects' />
            </ul>
            
            <div class='mx-2'>
            <Switch fallback={<></>}>
                <Match when={selectedTab() === SelectedTab.Servers}>
                <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Minus iure impedit non voluptates tenetur porro molestiae quo harum at eos ullam, doloremque quam blanditiis facilis labore ad quod deserunt quas!</p>
                </Match>
            </Switch>
            </div>
        </div>
    );
};

export default Navbar;