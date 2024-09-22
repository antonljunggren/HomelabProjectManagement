export type ServerSecret = {
    id: string;
    secretName: string;
    secretValue: string;
}

export type ServerSpecification = {
    cpu: string;
    ramGigagabytes: number;
    diskSizeGigabytes: number;
}

export type Server = {
    id: string;
    name: string;
    ipAddress: string;
    serverSpecifications: ServerSpecification;
    secrets: ServerSecret[];
}

