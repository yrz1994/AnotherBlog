# AnotherBlog 
![MIT](https://img.shields.io/github/license/yrz1994/AnotherBlog)
![Github](https://img.shields.io/github/stars/yrz1994/AnotherBlog?style=social)

Another Blog是一个采用微服务架构的练手项目，前端使用[Blazor Webassembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)开发（待开发），后端服务使用[.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)开发。实体模型采用DDD思想进行建模，并通过MediatR实现CQRS。

 - 使用[Ocelot](https://github.com/ThreeMammals/Ocelot)作为聚合网关；
 - 使用[Consul](https://www.consul.io/)进行服务注册与发现；
 - 使用[IdentityServer4](https://github.com/IdentityServer/IdentityServer4/blob/main/docs/index.rst)进行管理员角色的认证授权；
 - 数据库使用MySQL；
 - 使用Docker容器化部署；

部署脚本：
```
/*启动测试环境MySQL容器*/
docker pull mysql:latest
docker run -d -p 3306:3306 --name MySQL 
			-v mysql_volume:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456
			mysql:latest

/*启动Consul: */
docker-compose up -d

/*编译服务镜像: */
docker build -t article_service:dev -f .\AnotherBlog.ArticleAPI\Dockerfile .
docker build -t comment_service:dev -f .\AnotherBlog.CommentAPI\Dockerfile .

/*启动服务: */
docker run -d -p 5010:80 —name article_service_1 article_service:dev —scheme="http" —ip="host.docker.internal" —port="5010"

docker run -d -p 5020:80 —name comment_service_1 comment_service:dev —scheme="http" —ip="host.docker.internal" —port="5020"
```

```sql
CREATE SCHEMA `blog` DEFAULT CHARACTER SET utf8mb4 ;
CREATE SCHEMA `identity` DEFAULT CHARACTER SET utf8mb4 ;
```

```
/*IdentityServer DB迁移(设置IdentityServer项目为启动项):*/
PM>Add-Migration initPersistedGrantDb -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb

PM>Add-Migration initConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb

PM>Update-Database -Context PersistedGrantDbContext

PM>Update-Database -Context ConfigurationDbContext

/*Administrator DB迁移(设置IdentityServer项目为启动项) 
默认项目选择AnotherBlog.Infra.Data:*/
PM>Add-Migration initUserDb -c UserContext -o Migrations/User/UserDb

PM>Update-Database -Context UserContext

/*Blog DB迁移(设置AritcleAPI项目为启动项) 
默认项目选择AnotherBlog.Infra.Data:*/
PM>Add-Migration initBlogDb -c BlogContext -o Migrations/Blog

PM>Update-Database -Context BlogContext
```

```
/*OpenSSL Self-signed certificate:*/
1.Set private key
	openssl genrsa -out private_ids.key 2048
2.Set public key
	openssl req -new -x509 -key private_ids.key -days 3650 -out public_ids.crt
3.Export .pfx
	openssl pkcs12 -export -in public_ids.crt -inkey private_ids.key -out ids.pfx
4.Set export password:
	123456 (For Demo)
```

