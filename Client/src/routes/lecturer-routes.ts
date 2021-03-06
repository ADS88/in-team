import CoursePage from "../components/courses/CoursePage"
import CoursesPage from "../components/courses/CoursesPage"
import TeamPage from "../components/courses/TeamPage"
import AlphasPage from "../components/alphas/AlphasPage"
import AlphaPage from "../components/alphas/AlphaPage"
import IRoute from "./IRoute"
import StatePage from "../components/alphas/StatePage"
import SurveyPage from "../components/surveys/SurveyPage"
import CreateSurveyPage from "../components/surveys/CreateSurveyPage"
import IterationPage from "../components/courses/IterationPage"
import GradeTeamPage from "../components/reviews/GradeTeamPage"

//Routes within the application that only lecturers can access
const lecturerRoutes: IRoute[] = [
  {
    path: "/course/:id",
    name: "Single course page",
    exact: true,
    component: CoursePage,
  },
  {
    path: "/team/:id",
    name: "Team page",
    exact: true,
    component: TeamPage,
  },
  {
    path: "/courses",
    name: "Courses page",
    exact: true,
    component: CoursesPage,
  },
  {
    path: "/alphas",
    name: "Alpha page",
    exact: true,
    component: AlphasPage,
  },
  {
    path: "/alpha/:id",
    name: "Team page",
    exact: true,
    component: AlphaPage,
  },
  {
    path: "/state/:id",
    name: "State page",
    exact: true,
    component: StatePage,
  },
  {
    path: "/surveys",
    name: "Surveys page",
    exact: true,
    component: SurveyPage,
  },
  {
    path: "/createsurvey",
    name: "Create Survey page",
    exact: true,
    component: CreateSurveyPage,
  },
  {
    path: "/",
    name: "Survey page",
    exact: true,
    component: SurveyPage,
  },
  {
    path: "/course/:courseId/iteration/:iterationId",
    name: "Iteration page",
    exact: true,
    component: IterationPage,
  },
  {
    path: "/team/:teamId/gradeteam/:iterationId",
    name: "Iteration page",
    exact: true,
    component: GradeTeamPage,
  },
]

export default lecturerRoutes
