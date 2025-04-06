# BuddhaChantGame
多人念佛游戏，通过语音识别记录佛号

# 游戏试玩
请加Q群获取1045985136
项目有疑问也可以加群交流

# 项目说明
## BuddhaGameBackend
BuddhaGameBackend是使用Python-Flask编写的后台程序，用来连接数据库，将用户信息进行持久化保存。主要逻辑在[user_controller.py](BuddhaGameBackend/blueprints/user_controller.py)
数据库（Mysql）的键表请查看[models.py](BuddhaGameBackend/model/models.py)
数据库的连接配置请查看[static_config.py](BuddhaGameBackend/static_config.py)

## BuddhaGameServer
BuddhaGameServer是使用Java编写的后台程序，使用UDP协议和客户端通讯。

在[GlobalConfig.java](BuddhaGameServer/src/global/GlobalConfig.java)中配置HTTP_SEVER_IP（上面的Flask后台的地址）

在[tasks](BuddhaGameServer/assets/tasks)文件夹中，可以编辑、添加新的任务，任务都是以Json格式编写

## BuddhaGameClient
BuddhaGameClient是使用Unity编写的客户端。

注意：该项目需要第三方库Recognissimo的支持，这个库是unity商店收费的项目，所以我没有放上来。

请在[NetworkFactory.cs](BuddhaGameClient/Assets/Scripts/Network/NetworkFactory.cs)分别配置好BACKEND_SERVER_HOST（上面的Flask后台的地址）和UDP_SERVER_HOST（上面的Java UDP后台的地址）





