import { useContext, useEffect, useState } from "react";
import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
  Pagination,
} from "@mui/material";
import {
  deleteSoftware,
  getAllSoftwaresWithShop,
} from "../../services/software";
import { ISoftwareWithShop } from "../../interfaces/Software/ISoftwareWithShop";
import { NotificationContext } from "../../utils/context/NotificationContext";

const AdminSoftwaresList = () => {
  const [softwares, setSoftwares] = useState<ISoftwareWithShop[]>();
  const [refresh, setRefresh] = useState(false);
  const { success } = useContext(NotificationContext);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [page, setPage] = useState<number>(1);

  const handleRefresh = () => {
    setRefresh(!refresh);
  };

  const handlePageChange = (
    event: React.ChangeEvent<unknown>,
    value: number
  ) => {
    setPage(value);
  };

  const handleDelete = (softwareId: number, shopId: number) => {
    deleteSoftware({ softwareId: softwareId, shopId: shopId }).then(() => {
      handleRefresh();
      success("Successfully deleted software!");
    });
  };

  useEffect(() => {
    getAllSoftwaresWithShop().then((softwareData) => {
      if (softwareData) {
        setSoftwares(softwareData.data);
        setTotalPages(softwareData.pagination.TotalPages);
        setPage(softwareData.pagination.CurrentPage);
      }
    });
  }, [refresh, page]);

  return (
    <>
      <h2>All softwares</h2>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell>Shop Id</TableCell>
              <TableCell>Shop Name</TableCell>
              <TableCell>Software Name</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {softwares &&
              softwares.length > 0 &&
              softwares.map((software) => (
                <TableRow
                  key={software.id}
                  // sx={{
                  //   cursor: "pointer",
                  //   "&:hover": {
                  //     backgroundColor: (theme) => theme.palette.action.hover,
                  //   },
                  // }}
                  // onClick={() =>
                  //   navigate(
                  //     `/dashboard/shops/${subscription.software.shop.id}/softwares/${subscription.software.id}`
                  //   )
                  // }
                >
                  <TableCell>{software.id}</TableCell>
                  <TableCell>{software.shop.id}</TableCell>
                  <TableCell>{software.shop.name}</TableCell>
                  <TableCell>{software.name}</TableCell>
                  <TableCell>
                    <Button
                      color="warning"
                      onClick={() =>
                        handleDelete(software.id, software.shop.id)
                      }
                    >
                      Delete
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Pagination
        style={{ marginTop: "16px" }}
        count={totalPages}
        page={page}
        onChange={handlePageChange}
      />
    </>
  );
};

export default AdminSoftwaresList;
