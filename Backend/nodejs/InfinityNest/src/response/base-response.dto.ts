import { ApiProperty } from '@nestjs/swagger';

export class BaseResponse<T> {
  @ApiProperty()
  data: T;

  @ApiProperty()
  message: string;
}