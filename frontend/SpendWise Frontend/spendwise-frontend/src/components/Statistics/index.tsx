import { FC, useEffect, useState } from "react";

import { CategorySpending } from "../shared/types/CategorySpending";
import { CategoriesApiClient } from "../../api/Clients/CategoriesApiClients";
import { CategorySpendingModel } from "../../api/Models/CategorySpendingModel";
import { SetDateIntervalPopup } from "./SetDateIntervalPopup";
import { Box, Button, Divider, Typography } from "@mui/material";
import { format } from "date-fns";
import "chart.js/auto";

import "./Statistics.css";
import { PieChart } from "./Charts/PieChart";
import { BarChart } from "./Charts/BarChart";
import { DoughnutChart } from "./Charts/DoughnutChart";

export const Statistics: FC = () => {
  const [categorySpendings, setCategorySpendings] = useState<
    CategorySpending[]
  >([]);
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);
  const [openSetDateInterval, setOpenDateInterval] = useState(false);
  const [chartType, setChartType] = useState<"pie" | "bar" | "doughnut">("pie");

  const handleOpenSetDateInterval = () => setOpenDateInterval(true);
  const handleCloseSetDateInterval = () => setOpenDateInterval(false);

  const processData = (data: CategorySpending[]) => {
    return {
      labels: data.map((item) => item.Name),
      datasets: [
        {
          label: "Spending",
          data: data.map((item) => item.TotalSpent),
          backgroundColor: [
            "rgba(255, 99, 132, 0.6)",
            "rgba(54, 162, 235, 0.6)",
            "rgba(255, 206, 86, 0.6)",
            "rgba(75, 192, 192, 0.6)",
            "rgba(153, 102, 255, 0.6)",
            "rgba(255, 159, 64, 0.6)",
            "rgba(155, 159, 64, 0.6)",
            "rgba(0, 128, 0, 0.6)",
            "rgba(255, 0, 0, 0.6)",
          ],
        },
      ],
    };
  };

  const spendingData = processData(categorySpendings);

  const fetchSavings = async (start: Date | null, end: Date | null) => {
    try {
      const res = await CategoriesApiClient.getSpendingAsync(start, end);
      const spendings = res.map(
        (e: CategorySpendingModel) => ({ ...e } as CategorySpending)
      );
      setCategorySpendings(spendings);
    } catch (error: any) {
      console.log(error);
    }
  };

  const renderChart = () => {
    switch (chartType) {
      case "pie":
        return <PieChart data={spendingData} />;
      case "bar":
        return <BarChart data={spendingData} />;
      case "doughnut":
        return <DoughnutChart data={spendingData} />;
      default:
        return null;
    }
  };

  useEffect(() => {
    fetchSavings(startDate, endDate);
  }, [startDate, endDate]);

  return (
    <Box className={"statistics-page-container"}>
      <Box className={"statistics-title-text"}>Statistics</Box>

      <Divider />

      <Box className={"statistics-date-interval-container"}>
        <Button
          variant="contained"
          className={"statistics-date-interval-button"}
          onClick={handleOpenSetDateInterval}
        >
          <Typography>Set Date Interval</Typography>{" "}
        </Button>
      </Box>

      <Box className={"date-intervals"}>
        <Typography>
          Start Date:{" "}
          {startDate ? format(startDate, "dd/MM/yyyy") : "No defined"}
        </Typography>
        <Typography>
          End Date: {endDate ? format(endDate, "dd/MM/yyyy") : "No defined"}
        </Typography>
      </Box>

      <Box className={"statistics-buttons"}>
        <Button
          variant="contained"
          onClick={() => setChartType("pie")}
          disabled={chartType === "pie"}
        >
          Pie Chart
        </Button>
        <Button
          variant="contained"
          onClick={() => setChartType("bar")}
          disabled={chartType === "bar"}
        >
          Bar Chart
        </Button>
        <Button
          variant="contained"
          onClick={() => setChartType("doughnut")}
          disabled={chartType === "doughnut"}
        >
          Doughnut Chart
        </Button>
      </Box>

      <Box className={"statistics-wrapper"}>
        <Box className={"statistics-graph-container"}>
          <Box className={"statistics-title-text"}>
            Total Spending:{" "}
            {categorySpendings
              .reduce((acc, item) => acc + item.TotalSpent, 0)
              .toFixed(2)}
          </Box>
          {renderChart()}
        </Box>
      </Box>

      <SetDateIntervalPopup
        open={openSetDateInterval}
        onClose={handleCloseSetDateInterval}
        onEditing={(startDate, endDate) => {
          setStartDate(startDate);
          setEndDate(endDate);
          setOpenDateInterval(false);
        }}
      />
    </Box>
  );
};
