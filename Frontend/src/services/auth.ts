import api from "./api";

type authParams = {
  email: string;
  password: string;
};

export const login = async ({ email, password }: authParams) => {
  try {
    const response = await api.post("auth/login", { email, password });

    if (response.status === 200) {
      return response.data.accessToken;
    }

    return null;
  } catch {
    return null;
  }
};

export const registerBuyer = async ({
  email,
  password,
}: authParams): Promise<string | null> => {
  try {
    const response = await api.post("auth/register/user", { email, password });

    if (response.status === 200) {
      return response.data.accessToken;
    }

    return null;
  } catch {
    return null;
  }
};

export const registerSeller = async ({
  email,
  password,
}: authParams): Promise<string | undefined> => {
  try {
    const response = await api.post("auth/register/seller", {
      email,
      password,
    });

    if (response.status === 200) {
      return response.data.accessToken;
    }

    return undefined;
  } catch {
    return undefined;
  }
};
