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
	return 'xyxyy'.replace(/[xy]/g,function(c) {
		var r = Math.random()*16|0, v = c =='x'? r : (r&0x3|0x8);
		return v.toString(16);
	});
}

//when socker connected
io.on('connection', function(socket){
	var id = "";//client's own id
	
	//when recieves login action from client
	socket.on('login', (data,callback) => {
		//prevent change name
		 delete ids[ids.indexOf(id)];
		//client's id can not contains word 'system' as not effect on some system features
		if(data.id.toLowerCase().indexOf('system') >= 0){
			//randomize a id for the client
			id = guid();
	    	while(ids.indexOf(id) >= 0)
	    	{
	    		id = guid();
	    	}
		}
		//if id list doesnt contains client's id, which means the client will be able to use this id
		else if(ids.indexOf(data.id) == -1)
	    {
	    	id = data.id;
	    }
	    //if already contains this name, randomize a new id bases on client's id
	    else{
	    	var n_id = data.id+"-"+guid();
	    	id = n_id;
	    	while(ids.indexOf(id) >= 0)
	    	{
	    		id = data.id+"-"+guid();
	    	}
	    }
	    //send a callback which tells client's current id
	    callback(id);
	    ids.push(id);
	    //tells people in the chatroom that who joined
	    io.sockets.emit('recieveMsg', {id: "System",text: id+" has joined thet chat!"});
	    console.log(id+" has now joined!");
  });
	
	//when a client request to send message
	socket.on('sendMsg', (msg,callback) => {
		//if the client has not logged in, the client is not able to send any message
	    if(ids.indexOf(msg.id) == -1)
	    {
	        callback("Invalid id, are you sure that you have logged in?");
	        return;
	    }
        io.sockets.emit('recieveMsg', msg);
        var chat = msg.id+" : "+msg.text;
        fs.appendFile('history.txt',chat+'\n',function(error){
			if(error){
        		console.log(error);
        		return false;
    		}
    		console.log(chat);
		});
	});
  
  //when a client leave, remove his id in the chatroom
  socket.on('disconnect', function () {
        delete ids[ids.indexOf(id)];
    });
})
