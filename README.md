# AnotherBlog

启动MySQL容器:
docker pull mysql:latest
docker run -d -p 3306:3306 --name MySQL -v mysql_volume:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 mysql:latest

AnotherBlog.GatewayAPI:
Address:https://localhost:5001;

启动Consul: 
docker-compose up -d

编译服务镜像:
docker build -t article_service:dev -f .\AnotherBlog.ArticleAPI\Dockerfile .
docker build -t comment_service:dev -f .\AnotherBlog.CommentAPI\Dockerfile .

启动服务:
docker run -d -p 5010:80 --name article_service_1 article_service:dev --scheme="http" --ip="host.docker.internal" --port="5010"
docker run -d -p 5011:80 --name article_service_2 article_service:dev --scheme="http" --ip="host.docker.internal" --port="5011"
docker run -d -p 5012:80 --name article_service_3 article_service:dev --scheme="http" --ip="host.docker.internal" --port="5012"
docker run -d -p 5020:80 --name comment_service_1 comment_service:dev --scheme="http" --ip="host.docker.internal" --port="5020"
docker run -d -p 5021:80 --name comment_service_2 comment_service:dev --scheme="http" --ip="host.docker.internal" --port="5021"
docker run -d -p 5022:80 --name comment_service_3 comment_service:dev --scheme="http" --ip="host.docker.internal" --port="5022"

Init database:
CREATE SCHEMA `blog` DEFAULT CHARACTER SET utf8mb4 ;

CREATE SCHEMA `identity` DEFAULT CHARACTER SET utf8mb4 ;

IdentityServer DB迁移(设置IdentityServer项目为启动项):
PM>Add-Migration initPersistedGrantDb -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
PM>Add-Migration initConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
PM>Update-Database -Context PersistedGrantDbContext
PM>Update-Database -Context ConfigurationDbContext

PM>Add-Migration initUserDb -c UserContext -o Migrations/User/UserDb
PM>Update-Database -Context UserContext

OpenSSL Self-signed certificate:
1.Set private key
	openssl genrsa -out private_ids.key 2048
2.Set public key
	openssl req -new -x509 -key private_ids.key -days 3650 -out public_ids.crt
3.Export .pfx
	openssl pkcs12 -export -in public_ids.crt -inkey private_ids.key -out ids.pfx
4.Set export password:
	123456 (For Demo)