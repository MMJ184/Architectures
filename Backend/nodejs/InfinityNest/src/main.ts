import { NestFactory, Reflector } from '@nestjs/core';
import { AppModule } from './app.module';
import {
  DocumentBuilder,
  SwaggerModule,
} from '@nestjs/swagger';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  // app.useGlobalGuards(new JwtAuthGuard(new Reflector()));
  const config = new DocumentBuilder()
    .setTitle('Demo Api')
    .setDescription('The Demo API description')
    .setVersion('1.0')
    .addTag('Demo')
    .addBearerAuth({ type: 'http', scheme: 'bearer', bearerFormat: 'JWT' },
    'Authorization',)
    .build();
  const document = SwaggerModule.createDocument(app, config);

  SwaggerModule.setup('api', app, document);
  await app.listen(3000);
}
bootstrap();
