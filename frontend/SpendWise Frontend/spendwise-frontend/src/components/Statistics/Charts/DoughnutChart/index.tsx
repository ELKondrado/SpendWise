import { FC } from "react";
import { Box } from "@mui/material";
import { Doughnut } from "react-chartjs-2";

import "./DoughnutChart.css";

interface DoughnutChartProps {
  data: {
    labels: string[];
    datasets: {
      label: string;
      data: number[];
      backgroundColor: string[];
    }[];
  };
}

export const DoughnutChart: FC<DoughnutChartProps> = ({ data }: DoughnutChartProps) => {
  return (
    <Box className={"doughnut-chart"}>
      <Doughnut data={data} />
    </Box>
  );
};
