// auth/auth.controller.ts
import {
  Controller,
  Post,
  Body,
  ValidationPipe,
  Req,
  UseGuards,
} from '@nestjs/common';
import { AuthService } from '../service/auth.service';
import { LoginDto } from '../dto/login.dto';
import { access } from 'fs';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { UserService } from 'src/app/user/service/user.service';
import { request } from 'http';
import { JwtAuthGuard } from '../jwt-auth.guard';

@ApiTags('/Auth')
@Controller('auth')
export class AuthController {
  constructor(
    private readonly authService: AuthService,
    private readonly userService: UserService,
  ) {}

  @Post('login')
  async login(
    @Body(ValidationPipe) loginDto: LoginDto,
  ): Promise<{ data: any }> {
    const userData = await this.userService.login(loginDto);
    if (!userData) {
      let data = {
        isSuccess: true,
        data: [],
        error: 'Email/Password Are Not Valid',
      };
      return {
        data: data,
      };
    }

    console.log('userdata', userData, userData.email);
    const payload = { email: userData.email, sub: 1 }; // You can use any payload data you need

    const accessToken = await this.authService.generateToken(payload);
    const tokens = await this.authService.getTokens(userData.email);

    return { data: tokens };
  }
  @UseGuards(JwtAuthGuard)
  @ApiBearerAuth('Authorization')
  @Post('Refresh-token')
  async RefreshToken(@Req() req: any) {
    console.log('Request Headers:', req.headers?.authorization);
    let jwtToken = req.headers?.authorization.split(' ')[1];
    console.log('jwtToken', jwtToken);
    return null;
  }
}
