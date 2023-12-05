import api from "./api";

type refreshParams = {
  accessToken: string;
  refreshToken: string;
};

export const refresh = async ({ accessToken, refreshToken }: refreshParams) => {
  try {
    const response = await api.post("auth/refresh", {
      accessToken,
      refreshToken,
    });

    if (response.status === 200) {
      const responseData: LoginResponse = response.data;
      return responseData;
    }

    return null;
  } catch {
    return null;
  }
};

type authParams = {
  email: string;
  password: string;
};

export const login = async ({ email, password }: authParams) => {
  try {
    const response = await api.post("auth/login", { email, password });

    if (response.status === 200) {
      const responseData: LoginResponse = response.data;
      return responseData;
    }

    return null;
  } catch {
    return null;
  }
};

export const registerBuyer = async ({
  email,
  password,
}: authParams): Promise<LoginResponse | null> => {
  try {
    const response = await api.post("auth/register/user", { email, password });

    if (response.status === 200) {
      const responseData: LoginResponse = response.data;
      return responseData;
    }

    return null;
  } catch {
    return null;
  }
};

export const registerSeller = async ({
  email,
  password,
}: authParams): Promise<LoginResponse | undefined> => {
  try {
    const response = await api.post("auth/register/seller", {
      email,
      password,
    });

    if (response.status === 200) {
      const responseData: LoginResponse = response.data;
      return responseData;
    }

    return undefined;
  } catch {
    return undefined;
  }
};
