// auth.service.ts
import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { JwtPayload } from '../dto/JwtPayload';
import { ConfigService } from '@nestjs/config';
import { jwtConstants, resfresh } from '../constants';

@Injectable()
export class AuthService {
  constructor(private readonly jwtService: JwtService,  private configService: ConfigService,) { }

  async generateToken(payload: JwtPayload): Promise<string> {
    return this.jwtService.sign(payload);
  }

  async generateTokenRefresh(payload: JwtPayload): Promise<string> {
    return this.jwtService.sign(payload);
  }

  getPayload(token: string): any {
    return (this.jwtService.decode(token.replace('Bearer ', '')) as any);
  }
  async getTokens( username: string) {
    const [accessToken, refreshToken] = await Promise.all([
      this.jwtService.signAsync(
        {
          sub: 1,
          username,
        },
        {
          secret: this.configService.get<string>(jwtConstants.secret),
          expiresIn: '15m',
        },
      ),
      this.jwtService.signAsync(
        {
          sub: 1,
          username,
        },
        {
          secret: this.configService.get<string>(resfresh.secret),
          expiresIn: '7d',
        },
      ),
    ]);

    return {
      accessToken,
      refreshToken,
    };
  }

  
}

