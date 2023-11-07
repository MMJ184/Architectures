import { BaseModel } from 'src/common/baseModel';
import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('users')
export class UserEntity extends BaseModel {
  @Column()
  name: string;

  @Column()
  email: string;

  @Column()
  password: string;
}