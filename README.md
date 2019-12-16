# kafka-sample-implementation
simple kafka implementation for .NET application using [KafkaNet](https://github.com/Jroland/kafka-net).


#steps:

	1- start zookeeper:
		./zookeeper-server-start.bat ../../config/zookeeper.properties
		
	2- start kafka server:
		./kafka-server-start.bat ../../config/server.properties

	3- create a topic:
		./kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic chat-message

	4- run app:
		dotnet clean
		dotnet build
		dotnet run
