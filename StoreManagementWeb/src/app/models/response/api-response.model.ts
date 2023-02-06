export interface PaginationModel<T> {
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
  totalPage: number;
  records: T[];
}
