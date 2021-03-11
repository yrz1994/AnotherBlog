CREATE SCHEMA `blog` DEFAULT CHARACTER SET utf8mb4 ;

CREATE SCHEMA `identity` DEFAULT CHARACTER SET utf8mb4 ;


PM>Add-Migration initPersistedGrantDb -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb

PM>Add-Migration initConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb