import React, { useState } from 'react';
import { BBQEquipmentTypeService } from 'shared-types/src/services/bbq-equipment-services';
import { BBQEquipmentType, CookType } from 'shared-types/src/types/recipee-types';

const BBQEquipmentEditor: React.FC = () => {
    const [equipmentType, setEquipmentType] = useState<BBQEquipmentType>(BBQEquipmentTypeService.createBlankBBQEquipmentType());
    const [message, setMessage] = useState<string>('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setEquipmentType(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleCookTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const { value } = e.target;
        setEquipmentType(prevState => ({
            ...prevState,
            cookTypes: [value as unknown as CookType]
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await BBQEquipmentTypeService.createBBQEquipmentType(equipmentType);
            setMessage('BBQ Equipment Type created successfully!');
            setEquipmentType(BBQEquipmentTypeService.createBlankBBQEquipmentType());
        } catch (error) {
            setMessage('Failed to create BBQ Equipment Type.'+ error);
        }
    };

    return (
        <div className="max-w-md mx-auto mt-10 p-6 bg-white rounded-lg shadow-md">
            <h2 className="text-2xl font-bold mb-4">Create BBQ Equipment Type</h2>
            <form onSubmit={handleSubmit}>
                <div className="mb-4">
                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="name">
                        Name
                    </label>
                    <input
                        type="text"
                        id="name"
                        name="name"
                        value={equipmentType.name}
                        onChange={handleChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="description">
                        Description
                    </label>
                    <textarea
                        id="description"
                        name="description"
                        value={equipmentType.description}
                        onChange={handleChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                        required
                    />
                </div>
                <div className="mb-4">
                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="imageUrl">
                        Image URL
                    </label>
                    <input
                        type="text"
                        id="imageUrl"
                        name="imageUrl"
                        value={equipmentType.imageUrl}
                        onChange={handleChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    />
                </div>
                <div className="mb-4">
                    <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="cookTypes">
                        Cook Type
                    </label>
                    <select
                        id="cookTypes"
                        name="cookTypes"
                        value={equipmentType.cookTypes[0] || ''}
                        onChange={handleCookTypeChange}
                        className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    >
                        {Object.keys(CookType)
                            .filter(type => isNaN(Number(type)))
                            .sort()
                            .map((type) => (
                                <option key={type} value={type}>
                                    {type}
                                </option>
                            ))}
                    </select>
                </div>
                <div className="flex items-center justify-between">
                    <button
                        type="submit"
                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
                    >
                        Save
                    </button>
                </div>
            </form>
            {message && <p className="mt-4 text-center text-green-500">{message}</p>}
        </div>
    );
};

export default BBQEquipmentEditor;