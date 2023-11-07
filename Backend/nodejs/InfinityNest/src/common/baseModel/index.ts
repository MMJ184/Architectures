import { Column, PrimaryGeneratedColumn } from "typeorm";

export class BaseModel {
    @PrimaryGeneratedColumn()
    id: number;

    @Column({default: false})
    isDisable:boolean

    @Column({default: new Date()})
    createdDate:Date

    @Column()
    createdBy:number

    @Column({nullable: true})
    modifiedDate?:Date

    @Column({nullable: true})
    modifiedBy?:number


}