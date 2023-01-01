using Autofac;
using Server.Entities;
using Server.Entities.Abstract;
using Server.Repositories;
using Server.Services;
using Server.Services.Abstract;

namespace Server;

public class ServerModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MessageMongoRepository>()
			.AsSelf()
			.SingleInstance();
		builder
			.RegisterType<UserMongoRepository>()
			.AsSelf()
			.SingleInstance();
		builder
			.RegisterType<MongoDbCollectionsProvider>()
			.As<IMongoDbCollectionsProvider>()
			.SingleInstance();
		builder
			.RegisterType<HardcodedApplicationConfiguration>()
			.As<IApplicationConfiguration>()
			.SingleInstance();
		builder
			.RegisterType<UserPasswordHashingService>()
			.As<IUserPasswordHashingService>()
			.SingleInstance();
	}
}