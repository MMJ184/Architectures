import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Post,
  Put,
  Req,
  UseGuards,
} from '@nestjs/common';
import { UserService } from '../service/user.service';
import { BaseResponse } from 'src/common/response/baseResponse';
import { UserEntity } from '../entities/user.entity';
import { ApiBearerAuth, ApiOkResponse, ApiTags } from '@nestjs/swagger';
import { UserDto } from '../dto/user.dto';
import { JwtAuthGuard } from 'src/app/auth/jwt-auth.guard';

@ApiTags('/User')
@Controller('/User')
@UseGuards(JwtAuthGuard)
@ApiBearerAuth('Authorization')
export class UserController {
  constructor(private readonly userService: UserService) {}
  @Get()
  @ApiOkResponse({ type: BaseResponse })
  async findAll(@Req() request: any): Promise<BaseResponse<UserEntity[]>> {
    console.log(request);
    const users = await this.userService.findAll();
    return new BaseResponse(users, 'User Get Successfully', true, '');
  }

  @Get(':id')
  @ApiOkResponse({ type: BaseResponse })
  async findOne(@Param('id') id: number): Promise<BaseResponse<UserEntity>> {
    const users = await this.userService.findOne(id);
    return new BaseResponse<UserEntity>(
      users,
      'User Get Successfully',
      true,
      '',
    );
  }

  @Post()
  @ApiOkResponse({ type: BaseResponse })
  async create(@Body() user: UserDto): Promise<BaseResponse<UserEntity>> {
    const createdUser = await this.userService.create(user);
    if (!createdUser) {
      return new BaseResponse<UserEntity>(
        createdUser,
        'User not Create',
        true,
        'User Not Create',
      );
    }
    return new BaseResponse<UserEntity>(
      createdUser,
      'User Create Successfully',
      true,
      '',
    );
  }

  @Put(':id')
  @ApiOkResponse({ type: BaseResponse })
  async update(
    @Param('id') id: number,
    @Body() user: UserDto,
  ): Promise<BaseResponse<UserEntity>> {
    const updatedUser = await this.userService.update(id, user);
    if (!updatedUser) {
      return new BaseResponse<UserEntity>(
        updatedUser,
        'User not update',
        true,
        'User not update',
      );
    }
    return new BaseResponse<UserEntity>(
      updatedUser,
      'User Update Successfully',
      true,
      '',
    );
  }

  @Delete(':id')
  @ApiOkResponse({ type: BaseResponse })
  async remove(@Param('id') id: number): Promise<BaseResponse<void>> {
    await this.userService.remove(id);
    return new BaseResponse(null, 'User deleted successfully.', true, '');
  }
}
