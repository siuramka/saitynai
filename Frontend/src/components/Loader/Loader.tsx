import { useContext } from "react";
import { CircularProgress } from "@mui/material";
import { LoaderContext } from "../../utils/context/LoaderContext";

const Loader = () => {
  const { loading } = useContext(LoaderContext);
  return (
    loading && (
      <div
        style={{
          position: "fixed",
          top: 0,
          left: 0,
          width: "100%",
          height: "100%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          backgroundColor: "#121212",
          zIndex: 9999,
        }}
      >
        <CircularProgress />
      </div>
    )
  );
};

export default Loader;
