import { MetaData } from './interface/metadata';
export class AppUser implements MetaData {
  CreatedBy: string;
  UpdatedBy: string;
  CreatedDate: Date;
  UpdatedDate: Date;
  DeleteFlag: number;
  id: number;

}
