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
import { IShop } from "../../interfaces/Shop/IShop";
import { deleteShop, getShops } from "../../services/shop";
import { NotificationContext } from "../../utils/context/NotificationContext";

const AdminShopsList = () => {
  const [shops, setShops] = useState<IShop[]>();
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

  const handleDelete = (shopId: number) => {
    deleteShop({ shopId: shopId }).then(() => {
      handleRefresh();
      success("Successfully deleted shop!");
    });
  };

  useEffect(() => {
    getShops(page).then((shopData) => {
      if (shopData) {
        setShops(shopData.data);
        setTotalPages(shopData.pagination.TotalPages);
        setPage(shopData.pagination.CurrentPage);
      }
    });
  }, [refresh, page]);

  return (
    <>
      <h2>All Shops</h2>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Contact</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {shops &&
              shops.length > 0 &&
              shops.map((shop) => (
                <TableRow
                  key={shop.id}
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
                  <TableCell>{shop.id}</TableCell>
                  <TableCell>{shop.name} mo</TableCell>
                  <TableCell>{shop.contactInformation}</TableCell>
                  <TableCell>
                    <Button
                      color="warning"
                      onClick={() => handleDelete(shop.id)}
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

export default AdminShopsList;
