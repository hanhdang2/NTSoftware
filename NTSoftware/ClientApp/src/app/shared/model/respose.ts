
export class GenericResult<T>  {
  Data: T;
  Success: boolean;
  Message: string;
  ErrorCode: number;
}
export class PagedResult<T>  {
  Results: Array<T>;
  CurrentPage: number;
  PageCount: number;
  PageSize: number;
  RowCount: number;
  FirstRowOnPage: number;
  LastRowOnPage: number;
}
