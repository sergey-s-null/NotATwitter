using Autofac;
using Server.Repositories;

namespace Server;

public class ServerModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder
			.RegisterType<MessageMongoRepository>()
			.AsSelf()
			.SingleInstance();
	}
}