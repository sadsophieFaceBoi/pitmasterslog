import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import {Recipee} from 'shared-types/src/types/recipee-types'
import BBQEquipmentEditor from './components/equipment/bbq-equipment-editors'
function App() {
  const [count, setCount] = useState(0)
  const [recipes, setRecipes] = useState<Recipee[]>([])
   useEffect(() => {
    // fetch('http://localhost:3000/recipees')
    //   .then((res) => res.json())
    //   .then((data) => setRecipes(data))
    setRecipes([])
  }, [])
  return (
    <>
      <h1>Recipes</h1>
      <ul>
        {recipes.map((recipe) => (
          <li key={recipe.id}>{recipe.name}</li>
        ))}
      </ul>
      <BBQEquipmentEditor/>
    </>
  )
}

export default App
