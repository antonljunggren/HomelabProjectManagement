# HomelabProjectManagement

This project is for keeping track of homelab servers and the applications that run on them. Could probarbly also be used for other servers by modifying the domain language.

## Features so far

Still very very early, so the features are at a bare minimum

### Servers:

- Can do CRUD on servers and serverSecrets via the server page
- All servers can be found in the sidebar menu

## Planned features:

- [x] Being able to CRUD Servers and server secrets
- [ ] Do CRUD on Applications and tie them to a server
- [ ] Do CRUD on Projects and tie them to one or multiple Applications
- [ ] Being able to add documentation to Applications, like simple .md files
- [ ] Being able to add health checks to the Servers / Applications

## Misc:

### Some future ideas (Scope creep)

- Able to publish .md readme files from repos to the system via CI/CD <br/>
  And maybe reverse, that the system can fetch from repos in case the system is not open to the network, but then the repos needs to be public maybe...
- more...
