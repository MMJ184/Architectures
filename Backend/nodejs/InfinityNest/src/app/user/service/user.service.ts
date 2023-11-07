import { Injectable, NotFoundException } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { UserEntity } from '../entities/user.entity';
import { Repository } from 'typeorm';
import { UserDto } from '../dto/user.dto';
import { LoginDto } from 'src/app/auth/dto/login.dto';

@Injectable()
export class UserService {
  constructor(
    @InjectRepository(UserEntity)
    private readonly userRepository: Repository<UserEntity>
  ) {}
  async findAll(): Promise<UserEntity[]> {
    return this.userRepository.find();
  }

  async findOne(id: number): Promise<UserEntity> {
    let User = await this.userRepository.findOne({where:{id}});
    if (!User) {
      throw new NotFoundException('User not found');
    }
    return User
  }

  async create(user: UserDto): Promise<UserEntity> {
    user.createdBy = 1
    let createdUser = await this.userRepository.save(user);
    console.log("createdUser",createdUser)
    return createdUser
  }

  async update(id: number, user: UserDto): Promise<UserEntity> {
    await this.userRepository.update(id, user);
    return this.findOne(id);
  } 

  async remove(id: number): Promise<void> {
    await this.userRepository.delete(id);
  }

  async login(user: LoginDto): Promise<any> {
    const users = await this.userRepository.findOneBy({email :user.email,password:user.password });
    return users
  }
}
