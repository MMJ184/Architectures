import { Module } from '@nestjs/common';
import { JwtModule, JwtService } from '@nestjs/jwt';
import { PassportModule } from '@nestjs/passport';
import { TypeOrmModule } from '@nestjs/typeorm';
import { AuthService } from '../service/auth.service';
import { AuthController } from '../controller/auth.controller';
import { JwtAuthGuard } from '../jwt-auth.guard';
import { jwtConstants } from '../constants';
import { JwtStrategy } from '../jwt.strategy';
import { RefreshTokenStrategy } from '../refreshToken.strategy';
import { ConfigModule } from '@nestjs/config';
import { UserModule } from 'src/app/user/module/user.module';




@Module({
  imports: [
    JwtModule.register({
      secret: jwtConstants.secret, // Replace with your own secret key
      signOptions: { expiresIn: '1h' }, // Token expiration time (optional)
    }),UserModule,ConfigModule.forRoot()
  ],
  providers: [AuthService, JwtAuthGuard,JwtStrategy,RefreshTokenStrategy],
  controllers: [AuthController],
  exports: [JwtModule],
})
export class AuthModule { }
