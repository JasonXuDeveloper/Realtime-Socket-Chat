# Realtime-Chat
A realtime chat code based on C# client with Node.js Server, supports multi-chatroom &amp; private individual chats
<br>
[中文请点这里](#实时聊天系统)

## SETUP
### Server side
Note: This is a setup example on CentOS Server

Install Node.js
```sh
$ cd / # go to root dictionary
$ wget https://nodejs.org/dist/latest-v9.x/node-v9.4.0-linux-x64.tar.xz # download node.js v9 from official site
```

Decompress Node.js
```sh
$ tar -xvJf node-v9.4.0-linux-x64.tar.xz
```

Move it to /usr/local Dictionary
```sh
$ mv node-v9.4.0-linux-x64 /usr/local/node-v9
```

Create Symbolic Links
```sh
$ ln -s /usr/local/node-v9/bin/node /bin/node
```

Setup NPM
```sh
$ ln -s /usr/local/node-v9/bin/npm /bin/npm
```

Setup environment variable
```sh
$ echo 'export PATH=/usr/local/node-v9/bin:$PATH' >> /etc/profile
$ source /etc/profile
```

Use NPM
```sh
$ npm install forever -g
```

Install socket.io
```sh
$ npm install socket.io
```

Clone this project
```sh
$ git clone https://github.com/JasonXuDeveloper/Realtime-Chat.git
```

Go to Server dictionary
```sh
$ cd Server
```

Start the Server (make sure you have access to port 4567)
```sh
$ node main.js
Server start....
```

**Server side setup is now DONE!!!**


### Client side
Note: Client setup fits all operating systems which can install Unity3d (or you build project to different platforms via Unity)

1. Press [Here](https://github.com/JasonXuDeveloper/Realtime-Socket-Chat/tree/master) to go to our Github page (ignore if you are already here)
2. Download ZIP
3. Decompress ZIP
4. Go to Unity-DEMO/Asstes/Scenes
5. Double click file: "DEMO SCENE.unity"
6. Run it and enjoy your chatroom!

## C# Cores
#### Client.cs
Core class which sends messages and does server-side actions
```C#
//This is the base class on c# which is the core code
[Serializable]
public class Client
{
    //Use this method to set a new ID (Name) for a client
    public void SetID(string newID)
    {
        //xxx;
        //xxx;
    }
    
    //Login to server so that the client can send messages
    public void Login()
    {
        //xxx;
        //xxx;
    }
    
    //Use this method to send message
    public async void SendMessage(string msg)
    {
        //xxx
        //xxx
    }
}
/*
 * Run server code first
 * Then, try this code (or try the unity example)
 */
public class Main
{
    void Main()
    {
        var Client = new Client();
        Client.SetID("Test");
        Client.SendMessage("Hi There!");
        //See what will happen on the server
    }
}
```
<br>
#### Chatroom.cs
This is the main UI Setup & Server listener code.
See Unity-DEMO/Scripts/Chatroom.cs for details.

```c#
//What we have is the most imortant piece of code here:
Socket.On("recieveMsg",(e)=>
{
    var id = e.data.list[0].str;
    var text = e.data.list[1].str;
    //Codes here for showing what we recieved;
});
```
<br>
## Server cores
You can check the history.txt file in Server dictionary, which is the chat history overall
```js
var server=require('socket.io');//socket
var fs=require('fs');//file stream


//saves chat history
fs.access('history.txt',(err)=>{
    if(err){
        fs.writeFile('history.txt','','utf8',function(error){
            if(error){
                console.log(error);
                return false;
            }
            console.log('Successfully created history.txt');
        });
    }
});

var io = new server(4567);//you can change 4567 here to port you want


console.log('Server Start....');

var ids = [];//a list which saves current ids to avoid same names

//a function which makes unique id
function guid() {
    return xxx;
}

//when socker connected
io.on('connection', function(socket){
    //xxx;
}
```

## What will be coming soom?
* ~~C# based client source code (running on Unity 3d)~~
* ~~Node.js based server source code (running on Server, could be Virtual Machine, Node.js supports Windows, MaxOS and all kind of Linux)~~
* ~~Unity3d DEMO PROJECT~~
* Chat between individuals

## About developer
If you have any issues,
please follow [jason_the_developer](https://www.instagram.com/jason_the_programmer/) at instagram and send me messages!

<br><br><hr><br>


# 实时聊天系统
基于C#客户端以及Node.js服务端的聊天室，支持多人聊天室以及私人聊天

## 接下来会更新什么？
* 基于C#的客户端源代码（需要在Unity3d运行）
* 基于Node.js的服务端源代码（可在任何系统虚拟机运行，请先部署Node.js环境）
* Unity3d示例项目

## 部署
### 服务端
注意：该服务端部署仅适用于CentOS机器

下载Node.js
```sh
$ cd / # 切换到根目录
$ wget https://nodejs.org/dist/latest-v9.x/node-v9.4.0-linux-x64.tar.xz # 从官网下载
```

解压Node.js
```sh
$ tar -xvJf node-v9.4.0-linux-x64.tar.xz
```

移动解压文件到/usr/local Dictionary
```sh
$ mv node-v9.4.0-linux-x64 /usr/local/node-v9
```

创建软链接
```sh
$ ln -s /usr/local/node-v9/bin/node /bin/node
```

配置NPM
```sh
$ ln -s /usr/local/node-v9/bin/npm /bin/npm
```

配置环境变量
```sh
$ echo 'export PATH=/usr/local/node-v9/bin:$PATH' >> /etc/profile
$ source /etc/profile
```

使用NPM
```sh
$ npm install forever -g
```

下载socket.io
```sh
$ npm install socket.io
```

克隆该项目
```sh
$ git clone https://github.com/JasonXuDeveloper/Realtime-Chat.git
```

进入服务端目录
```sh
$ cd Server
```

启动服务器（确保端口4567可用）
```sh
$ node main.js
Server start....
```

**服务端配置完成！！！**

### 客户端
注意：客户端可以在任何可以使用Unity3d的操作系统运行（或者通过Unity打包项目到其他平台）

1. 点击[这里](https://github.com/JasonXuDeveloper/Realtime-Socket-Chat/tree/master)进入项目Github地址（如果你已经在这个页面请忽略）
2. 下载项目ZIP
3. 解压ZIP
4. 进入目录Unity-DEMO/Asstes/Scenes
5. 双击文件： "DEMO SCENE.unity"
6. 运行并尽情测试你的聊天室是否可用！

## C# 核心
#### Client.cs
处理服务器回调以及发送信息的核心代码
```C#
//This is the base class on c# which is the core code
[Serializable]
public class Client
{
    //Use this method to set a new ID (Name) for a client
    public void SetID(string newID)
    {
        //xxx;
        //xxx;
    }
    
    //Login to server so that the client can send messages
    public void Login()
    {
        //xxx;
        //xxx;
    }
    
    //Use this method to send message
    public async void SendMessage(string msg)
    {
        //xxx
        //xxx
    }
}
/*
 * Run server code first
 * Then, try this code (or try the unity example)
 */
public class Main
{
    void Main()
    {
        var Client = new Client();
        Client.SetID("Test");
        Client.SendMessage("Hi There!");
        //See what will happen on the server
    }
}
```

<br>

#### Chatroom.cs
展示UI以及接收服务器信息的核心代码
查看Unity-DEMO/Scripts/Chatroom.cs以获得更多详情

```c#
//What we have is the most imortant piece of code here:
Socket.On("recieveMsg",(e)=>
{
    var id = e.data.list[0].str;
    var text = e.data.list[1].str;
    //Codes here for showing what we recieved;
});
```

<br>

## 服务端核心
可以在Server目录下History.txt查阅完整全部客户聊天记录
```js
var server=require('socket.io');//socket
var fs=require('fs');//file stream


//saves chat history
fs.access('history.txt',(err)=>{
    if(err){
        fs.writeFile('history.txt','','utf8',function(error){
            if(error){
                console.log(error);
                return false;
            }
            console.log('Successfully created history.txt');
        });
    }
});

var io = new server(4567);//you can change 4567 here to port you want


console.log('Server Start....');

var ids = [];//a list which saves current ids to avoid same names

//a function which makes unique id
function guid() {
    return xxx;
}

//when socker connected
io.on('connection', function(socket){
    //xxx;
}
```

## 接下来会有什么？
* ~~基于C#的Unity3d客户端~~
* ~~Node.js服务端源代码~~
* ~~Unity3d实例项目~~
* 私聊解决方案

## 关于作者
有问题请加作者QQ：2313551611，
并标明是从Github来反馈问题的！




