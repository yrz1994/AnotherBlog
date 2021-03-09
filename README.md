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

PM>AnotherBlog.Identityt:
	Add-Migration InitialCreate
	Update-Database
