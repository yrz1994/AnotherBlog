# AnotherBlog

启动MySQL容器:
docker pull mysql:latest
docker run -d -p 3306:3306 --name MySQL mysql:latest
	-v mysql_volume:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456

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


初始化SQL:
CREATE SCHEMA `blog` DEFAULT CHARACTER SET utf8mb4 ;

CREATE SCHEMA `identity` DEFAULT CHARACTER SET utf8mb4 ;

IdentityServer DB迁移:
PM>Add-Migration initPersistedGrantDb -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
PM>Add-Migration initConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
PM>Update-Database -Context PersistedGrantDbContext
PM>Update-Database -Context ConfigurationDbContext

OpenSSL自签名证书:
1.私钥
	openssl genrsa -out private_ids.key 2048
2.公钥
	openssl req -new -x509 -key private_ids.key -days 3650 -out public_ids.crt
3.公钥及私钥的提取加密
	openssl pkcs12 -export -in public_ids.crt -inkey private_ids.key -out ids.pfx