import { createContext, useState } from "react";
import { Props } from "./AuthContext";

type LoaderContextType = {
  loading: boolean;
  setLoaderHandler: (loading: boolean) => void;
};
const initialLoaderContext: LoaderContextType = {
  loading: false,
  setLoaderHandler: () => {},
};
const LoaderContext = createContext<LoaderContextType>(initialLoaderContext);

const LoaderContextProvider = ({ children }: Props) => {
  const [loading, setLoading] = useState<boolean>(false);

  const setLoaderHandler = (loading: boolean) => {
    if (loading == false) {
      setTimeout(() => {
        setLoading(false);
      }, 600);
    } else {
      setLoading(true);
    }
  };

  const loaderContextValues: LoaderContextType = {
    loading,
    setLoaderHandler,
  };

  return (
    <LoaderContext.Provider value={loaderContextValues}>
      {children}
    </LoaderContext.Provider>
  );
};

export { LoaderContext, LoaderContextProvider };
