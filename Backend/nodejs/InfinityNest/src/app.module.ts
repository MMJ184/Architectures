import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { PassportModule } from '@nestjs/passport';
import { JwtModule, JwtService } from '@nestjs/jwt';
import { ConfigModule } from '@nestjs/config';
import { dataSourceOptions } from 'db/data-source';
import { UserModule } from './app/user/module/user.module';
import { AuthModule } from './app/auth/module/auth.module';
import { AuthService } from './app/auth/service/auth.service';

@Module({
  imports: [
    //   TypeOrmModule.forRoot({
    //   type: 'postgres',
    //   host: 'localhost',
    //   port: 5432,
    //   username: 'postgres',
    //   password: '1234',
    //   database: 'Demo',
    //   entities: ['dist/**/*.entity{.ts,.js}'],
    //   // synchronize: true, // This will automatically create database tables based on your entities (Only use in development)
    //   // namingStrategy: new SnakeNamingStrategy(),
    //   // ssl: true,
    //   // extra: {
    //   //   ssl: {
    //   //     rejectUnauthorized: false,
    //   //   },
    //   // },

    // }),
    // TypeOrmModule.forRoot(dataSourceOptions),
    TypeOrmModule.forRoot({
      type: 'mssql',
      host: '192.168.1.100',
      port: 1434,
      username: 'sa',
      password: 'Infinity!1234',
      database: 'Nestjs',
      entities: ['dist/**/*.entity{.ts,.js}'],
      extra: {
        options: {
          trustServerCertificate: true,
        },
      },
    }),
    UserModule,
    AuthModule,
    ConfigModule.forRoot(),
    // TypeOrmModule.forRoot(dataSourceOptions),
    PassportModule.register({ defaultStrategy: 'jwt' }),
    JwtModule.register({
      secret: 'secretKey', // Replace with your own secret key
      signOptions: { expiresIn: '1h' }, // Token expiration time (optional)
    }),
  ],
  controllers: [],
  providers: [AuthService],
})
export class AppModule {}
