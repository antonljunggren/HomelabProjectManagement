import { Component } from "solid-js";
import { Application } from "../../models/Application";
import { Server } from "../../models/Server";

type ApplicationNavbarItemProps = {
    application: Application,
    server?: Server
}

const ApplicationNavbarItem : Component<ApplicationNavbarItemProps> = (props) => {

    const application = props.application;
    const server = props.server;

    return (
        <div class="mt-2">
            <p class='pl-1 text-xs'>{server?.name} {server?.ipAddress}</p>
            <li class='hover:underline pl-3'>
                <a href={`/application/${application.id}`}>{application.applicationName} : {application.port}</a>
            </li>
        </div>
    );
};

export default ApplicationNavbarItem;