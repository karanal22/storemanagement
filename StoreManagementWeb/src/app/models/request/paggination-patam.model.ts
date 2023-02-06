export interface PaginationParam {
  pageNumber: number;
  pageSize: number;
  isActive?: boolean;
  sortBy?: { [key: string]: string; }[];
}
