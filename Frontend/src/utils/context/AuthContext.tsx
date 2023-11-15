import { jwtDecode } from "jwt-decode";
import React, { createContext, useState } from "react";

export type User = {
  id: string;
  role: string;
  email: string;
  exp: number;
};

export type AuthContextType = {
  user: User | null;
  setUserHandler: (token: string | null) => void;
  setUserSignout: () => void;
};

export const initialAuthContext: AuthContextType = {
  user: null,
  setUserHandler: () => {},
  setUserSignout: () => {},
};

export type Props = {
  children?: React.ReactNode;
};

type JwtPayload = {
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
  userId: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
  exp: number;
};

const getUserFromToken = (token: string) => {
  const decoded = jwtDecode<JwtPayload>(token);
  const user: User = {
    id: decoded.userId,
    role: decoded[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ],
    email:
      decoded[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ],
    exp: decoded.exp,
  };
  return user;
};

const AuthContext = createContext(initialAuthContext);

const AuthContextProvider = ({ children }: Props) => {
  const storedToken = localStorage.getItem("token");
  const initialUser = storedToken ? getUserFromToken(storedToken) : null;
  const [user, setUser] = useState<User | null>(initialUser);

  const setUserSignout = () => {
    localStorage.removeItem("token");
    setUserHandler(null);
  };

  // if (!lastLogin) {
  //   localStorage.setItem("lastLogin", Date.now().toString());
  // }

  // if (lastLogin && Date.now() - Date.parse(lastLogin) >= 2419200) {
  //   setUserSignout(); //just log out user after 1 month, gonna set jwt exp at 1.1month, should do this check via a backend call tbh
  // }

  const setUserHandler = (token: string | null) => {
    if (token) {
      const user = getUserFromToken(token);
      setUser(user);
      localStorage.setItem("token", token);
    } else {
      setUser(null);
      localStorage.removeItem("token");
    }
  };

  const authContextValues: AuthContextType = {
    user,
    setUserHandler,
    setUserSignout,
  };

  return (
    <AuthContext.Provider value={authContextValues}>
      {children}
    </AuthContext.Provider>
  );
};

export { AuthContext, AuthContextProvider };
