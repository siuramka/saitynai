import { IPagination } from "./IPagination";

export interface IPagedData<T> {
  data: T;
  pagination: IPagination;
}
