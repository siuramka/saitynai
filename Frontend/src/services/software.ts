import { ISoftware } from "../interfaces/Software/ISoftware";
import { CreateSoftware } from "../interfaces/Software/CreateSoftware";
import { EditSoftware } from "../interfaces/Software/EditSoftware";
import api from "./api";
import { ISoftwareWithShop } from "../interfaces/Software/ISoftwareWithShop";
import { IPagedData } from "../interfaces/IPagedData";
import { IPagination } from "../interfaces/IPagination";

export const getAllSoftwaresWithShop = async (page?: number) => {
  try {
    const endpoint = page ? `softwares?PageNumber=${page}` : "softwares";
    const response = await api.get<ISoftwareWithShop[]>(endpoint);
    
    if (response.status === 200) {
      const paginationHeader = response.headers["x-pagination"];
      if (paginationHeader) {
        const pagination: IPagination = JSON.parse(paginationHeader);
        const pagedData: IPagedData<ISoftwareWithShop[]> = {
          data: response.data,
          pagination: pagination,
        };
        return pagedData;
      }
    }

    return undefined;
  } catch {
    return undefined;
  }
};

export const getAllSoftwares = async () => {
  try {
    const response = await api.get<ISoftware[]>(`softwares`);
    if (response.status === 200) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

type shopSoftwareParams = {
  shopId: number;
  softwareId: number;
};

export const getShopSoftware = async ({
  shopId,
  softwareId,
}: shopSoftwareParams) => {
  try {
    const response = await api.get<ISoftware>(
      `shops/${shopId}/softwares/${softwareId}`
    );
    if (response.status === 200) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

type shopSoftwaresParams = {
  shopId: number;
};

export const getShopSoftwares = async ({ shopId }: shopSoftwaresParams) => {
  try {
    const response = await api.get<ISoftware[]>(`shops/${shopId}/softwares`);
    if (response.status === 200) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

type createShopSoftwareParams = {
  software: CreateSoftware;
  shopId: number;
};

export const createShopSoftware = async ({
  software,
  shopId,
}: createShopSoftwareParams) => {
  try {
    const response = await api.post<ISoftware>(
      `shops/${shopId}/softwares`,
      software
    );
    if (response.status == 201) {
      return response.data;
    }
    return undefined;
  } catch {
    return undefined;
  }
};

export type editSoftwareParams = {
  shopId: number;
  softwareId: number;
  software: EditSoftware;
};

export const editSoftware = async ({
  shopId,
  softwareId,
  software,
}: editSoftwareParams) => {
  try {
    const response = await api.put<ISoftware>(
      `shops/${shopId}/softwares/${softwareId}`,
      software
    );
    if (response.status === 200) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};

export type DeleteSoftwareParams = {
  shopId: number;
  softwareId: number;
};

export const deleteSoftware = async ({
  shopId,
  softwareId,
}: DeleteSoftwareParams) => {
  try {
    const response = await api.delete<ISoftware>(
      `shops/${shopId}/softwares/${softwareId}`
    );
    if (response.status === 204) {
      return response.data;
    }
  } catch {
    return undefined;
  }
};
