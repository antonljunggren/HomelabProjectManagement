export type Application = {
    id: string;
    serverId: string | null;
    applicationName: string;
    port: number;
    codeRepository: string;
}