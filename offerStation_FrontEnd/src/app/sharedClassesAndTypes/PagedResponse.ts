export interface PagedResponse<T> {
    data: T[];
    pageNumber: number;
    pageSize: number;
    totalCount: number;
    firstPage: string;
    lastPage: string;
    nextPage: string;
    previousPage: string;
  }
  