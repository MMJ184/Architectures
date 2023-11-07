import { ApiProperty } from "@nestjs/swagger";

// user/base-response.dto.ts
export class BaseResponse<T> {
    @ApiProperty({ description: 'Data returned by the response', type: Object })
    data: T;
  
    @ApiProperty({ description: 'Message describing the response', type: String })
    message: string;

    @ApiProperty({ description: 'error describing the response', type: String })
    error: string;
  
    @ApiProperty({ description: 'Flag indicating whether the request was successful', type: Boolean })
    isSuccess: boolean;
  
    constructor(data: T, message: string = 'Success', isSuccess: boolean = true , error:string) {
      this.data = data;
      this.message = message;
      this.isSuccess = isSuccess;
      this.error = error
    }
  }